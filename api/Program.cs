using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Embeddings;


var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

#region Configure Azure OpenAI Embedding API
string? deploymentName = Environment.GetEnvironmentVariable("EMB_AZURE_OPENAI_DEPLOYMENT_NAME");
// string? modelId = Environment.GetEnvironmentVariable("EMB_AZURE_OPENAI_MODEL_ID");
string? apiKey = Environment.GetEnvironmentVariable("EMB_AZURE_OPENAI_API_KEY");
string? endpoint = Environment.GetEnvironmentVariable("EMB_AZURE_OPENAI_ENDPOINT");

if (string.IsNullOrEmpty(deploymentName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(endpoint))
{
        throw new InvalidOperationException("OpenAI deployment name, model ID, API key, and endpoint expected to be set in environment variables.");
}

var modelId = "text-embedding-ada-002"; // ada @ 1536 < 3-large @ 256
modelId = "text-embedding-3-large"; // https://devblogs.microsoft.com/azure-sql/embedding-models-and-dimensions-optimizing-the-performance-resource-usage-ratio/

// Create a Semantic Kernel
#pragma warning disable SKEXP0010
var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddAzureOpenAITextEmbeddingGeneration(
        deploymentName: deploymentName,
        endpoint: endpoint,
        apiKey: apiKey,
        modelId: modelId // embedding model in our case, but otherwise can be any model including inference models
);

var semanticKernel = kernelBuilder.Build();

// After building the kernel, add this:
builder.Services.AddSingleton(semanticKernel);
#pragma warning disable SKEXP0001
builder.Services.AddSingleton(semanticKernel.GetRequiredService<ITextEmbeddingGenerationService>());
#pragma warning restore SKEXP0001
#endregion

var app = builder.Build();

app.Run();

