import styled from 'styled-components'
import Card from './components/Card'
import SearchBar from './components/SearchBar'

export const Container = styled.div`
    border: 1px solid #000;
    background: #a2a8d3;
    width: 100vw;
    height: 100vh;
    display: flex;
    align-items: center;
    justify-content: space-evenly;
    flex-wrap: wrap;
`;

function App() {
  

  return (
    <>
      <Container>
      <div>
        <Card></Card>
      </div>
      <div>
      <h2>Add a favorite city:</h2>
      <SearchBar></SearchBar>
      </div>  
      </Container>
    </>
  )
}

export default App
