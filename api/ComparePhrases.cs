using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.Embeddings;

namespace api
{
    public class ComparePhrases
    {
#pragma warning disable SKEXP0001
        private readonly ITextEmbeddingGenerationService _embeddingService;
#pragma warning restore SKEXP0001

        private readonly ILogger<ComparePhrases> _logger;

#pragma warning disable SKEXP0001
        public ComparePhrases(ILogger<ComparePhrases> logger, ITextEmbeddingGenerationService embeddingService)
        {
            _logger = logger;
            _embeddingService = embeddingService;
        }
#pragma warning restore SKEXP0001

        [Function("ComparePhrases")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation($"Function ⟪ComparePhrases⟫ → invoked with query string: «{req.QueryString}»");

            string? phrase1 = req.Query["phrase1"];
            string? phrase2 = req.Query["phrase2"];

            if (string.IsNullOrEmpty(phrase1) || string.IsNullOrEmpty(phrase2))
            {
                _logger.LogError("Function ⟪ComparePhrases⟫ → Missing required query parameters: phrase1 and/or phrase2.");
                return new BadRequestObjectResult("Please pass both phrase1 and phrase2 in the query string.");
            }

            var semcomp = new SemanticComparer(_embeddingService);
            var similarityVisual = await semcomp.CompareCosineSimilarityAsync(phrase1, phrase2);

            _logger.LogInformation($"Function ⟪ComparePhrases⟫ → Phrase1: {phrase1}, Phrase2: {phrase2}, Cosine Similarity: {similarityVisual}");
            return new OkObjectResult(similarityVisual);
        }
    }
}






#if false

Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("dog", "cat")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("dog", "puppy")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("puppy", "kitten")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("puppy", "calf")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("kitten", "calf")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("kitten", "calf")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("shin", "calf")}");

Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("baby dog", "puppy")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("kitten", "donut")}");

Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("arithmetic", "coal miner")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("fast", "slow")}");

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("happy", "ecstatic"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("happy", "miserable"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("democracy", "autocracy"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("quantum physics", "classical mechanics"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Pablo Picasso", "pablo picasso"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Pablo Picasso", "Picasso"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Pablo Picasso", "picasso"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Pablo Picasso", "Dave picasso"));

var modelString = $"┃ Embedding model used: {modelId} ┃";
var modelStringLine = $"━".PadLeft(modelString.Length - 2, '━');
Console.Error.WriteLine($"┣{modelStringLine}┫");
Console.Error.WriteLine(modelString);
Console.Error.WriteLine($"┣{modelStringLine}┫");

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("afaf.87sdf6asdf", "table cloth"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("banana", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayon", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("toilet paper", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Gothic architecture", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("pinball machine", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("button", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Dr. Seuss", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Dr. Seuss", "nuclear physicist"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayon bag", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("sheet of toilet paper", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayons", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("toilet paper sheet", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("toilet paper sheet", "nuclear physics textbook"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("toilet paper artwork", "nuclear physics equations"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayon box", "box of crayons"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayon boxes", "boxes of crayons"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hike", "jogging"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hike", "jog"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking", "jog"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking", "jogging"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking - woods", "jogging"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking", "jogging + woods"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking - woods", "jogging"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking", "walking"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking - woods", "walking"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking + on the street", "walking"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking - woods", "walking on the street"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("bathroom", "portapotty"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("bathroom", "porta potty"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("bathroom", "bath room"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("bathroom", "bathroom"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("bathroom", "rest room"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("bathroom", "restroom"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("king", "queen"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("queen", "king"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Queen", "King"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("queen", "king - man + woman"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("queen", "king + woman"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("queen", "king + woman + woman"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("extremely happy", "utterly devastated"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("complete success", "total failure"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("The sun is rising in the east.", "The sun is rising in the west."));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("The concept of being", "the concept of non-existence"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayon bag", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("toilet paper artwork", "nuclear physics equations"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayon bag", "toilet paper artwork"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayon bag", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("toilet paper artwork", "nuclear physics equations"));

// sundwudu is from Beuwulf
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("sundwudu", "toilet paper artwork"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("sundwudu", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("sundwudu", "nuclear physics equations"));


Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("kleenex", "tissue"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Kleenex", "tissue"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("drink", "beverage"));

var beuwulfPassage = """
Hæfde se goda Geata leoda
cempan gecorone þara þe he cenoste
findan mihte; fiftena sum,
sundwudu sohte, secg wisade,
lagucræftig mon landgemyrcu.
""";

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync(beuwulfPassage, "Sesame Street"));
// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync(beuwulfPassage, "nuclear physics equations"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync(beuwulfPassage, "nuclear physics"));

// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("king", "queen"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("queen", "king"));
// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Queen", "King"));
// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("queen", "king - man + woman"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("queen", "king + woman"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking", "walking"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking - woods", "walking"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking + on the street", "walking"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking - woods", "walking on the street"));

// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Orlando", "Orlando"));
// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Orlando", "orlando"));
// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("dog", "cat"));
// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("cat", "dog"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("dog", "perro")); // dog in Spanish
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("chat", "cat"));  // cat in French

// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Pablo Picasso", "pablo picasso"));
// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Pablo Picasso", "Picasso"));
// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Pablo Picasso", "picasso"));
// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Picasso", "Monet"));
// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Picasso", "Larry Bird"));
// Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Picasso", "Big Bird"));

#endif
