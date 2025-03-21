import { useState } from 'react'
import reactLogo from './assets/crankingai-logo.svg'
import viteLogo from '/crankingai-logo.svg'
import './App.css'

function App() {
  const [phrase1, setPhrase1] = useState('')
  const [phrase2, setPhrase2] = useState('')
  const [results, setResults] = useState('')

  const comparePhrases = async () => {
    try {
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
      setResults(data);
    } catch (error) {
      console.error('Error:', error);
      setResults('Error comparing phrases. Please try again.');
    }
  }

  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank" rel="noopener noreferrer">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank" rel="noopener noreferrer">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Text Comparison</h1>
      <div className="input-container">
        <input
          type="text"
          value={phrase1}
          onChange={(e) => setPhrase1(e.target.value)}
          placeholder="Enter first phrase"
        />
        <input
          type="text"
          value={phrase2}
          onChange={(e) => setPhrase2(e.target.value)}
          placeholder="Enter second phrase"
        />
      </div>
      <div className="button-container">
        <button onClick={comparePhrases}>Compare Phrases</button>
      </div>
      <div className="results-container">
        <textarea
          value={results}
          readOnly
          placeholder="Comparison results will appear here..."
          rows={4}
        />
      </div>
    </>
  )
}

export default App
