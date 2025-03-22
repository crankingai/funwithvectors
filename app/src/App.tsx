import { useState, useRef, useEffect } from 'react'
import reactLogo from './assets/crankingai-logo.svg'
import viteLogo from '/crankingai-logo.svg'
import './App.css'

function App() {
  const [phrase1, setPhrase1] = useState('')
  const [phrase2, setPhrase2] = useState('')
  const [results, setResults] = useState('')
  const resultsRef = useRef<HTMLTextAreaElement>(null)

  const comparePhrases = async () => {
    try {
      const functionBaseUrl = import.meta.env.VITE_API_URL || '/api';
      const functionBaseUrl = import.meta.env.VITE_API_URL || '/api';
      console.log('ComparePhrases API Base URL:', functionBaseUrl);
      const queryParams = new URLSearchParams({
        phrase1: phrase1,
        phrase2: phrase2
      }).toString();

      const functionApiUrl = `${functionBaseUrl}?${queryParams}`;
      console.log('ComparePhrases API URL:', functionApiUrl);

      const response = await fetch(functionApiUrl, {
        method: 'GET',
        headers: {
          'Accept': 'text/plain'
        }
      });

      const data = await response.text();
      setResults(prevResults => {
        // const timestamp = new Date().toLocaleTimeString();
        return `${prevResults}${prevResults ? '\n\n' : ''}[${data}`;
      });
    } catch (error) {
      console.error('Error:', error);
      setResults(prevResults => {
        const timestamp = new Date().toLocaleTimeString();
        return `${prevResults}${prevResults ? '\n\n' : ''}[${timestamp}] Error comparing phrases. Please try again.`;
      });
    }
  }

  const handleKeyPress = (event: React.KeyboardEvent) => {
    if (event.key === 'Enter') {
      comparePhrases();
    }
  };

  useEffect(() => {
    if (resultsRef.current) {
      resultsRef.current.scrollTop = resultsRef.current.scrollHeight
    }
  }, [results])

  return (
    <>
      <h2>Using OpenAI's <em>text-embedding-3-large</em> model with vector length 3072 floats.</h2>

      <div className="header-container">
        <a href="https://vite.dev" target="_blank" rel="noopener noreferrer">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <h1 className="title">🥳 Fun with ⟨Vectors⟩ ↖↑↗ 🤖</h1>
        <a href="https://react.dev" target="_blank" rel="noopener noreferrer">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <div className="input-container">
        <input
          type="text"
          value={phrase1}
          onChange={(e) => setPhrase1(e.target.value)}
          onKeyPress={handleKeyPress}
          placeholder="Enter first phrase"
        />
        <input
          type="text"
          value={phrase2}
          onChange={(e) => setPhrase2(e.target.value)}
          onKeyPress={handleKeyPress}
          placeholder="Enter second phrase"
        />
      </div>
      <div className="button-container">
        <button onClick={comparePhrases}>Compare Phrases</button>
      </div>
      <div className="results-container">
        <textarea
          ref={resultsRef}
          value={results}
          readOnly
          placeholder="Comparison results will appear here..."
          rows={12}
        />
      </div>
      <footer className="footer">
        <p>View the source code on <a href="https://github.com/crankingai/funwithvectors" target="_blank" rel="noopener noreferrer">GitHub</a></p>
      </footer>
    </>
  )
}

export default App
