import React, {useState, useEffect} from 'react';
import styled from 'styled-components';
import axios from 'axios';

export const Wrapper = styled.div`
    background-color: white;
    width: 100%;
    margin: auto;
    box-shadow: 0px 0px 8px;
    padding: 0 15px;
    display: flex;
    align-items: center;
    flex-direction: column;
`;
export const Input = styled.input`
  font-family: 'Open Sans', 'Helvetica Neue', 'Segoe UI', 'Calibri', 'Arial', sans-serif;
  font-size: 1.3rem;
  height: 100%;
  width: 60%;
  border: none;
  &:focus {
    outline: none;
  }
`;
export const ItemList = styled.div`
  padding: 3px;
  text-align: center;
  border-radius: 10px;
  &:hover{
    background-color: #ddd;
  }
`;

export const GET_CITIES = "https://localhost:7238/weather";

function SearchBar () {
  const [input, setInput] = useState('');
  const [cities, setCities] = useState({ data: [] });

  useEffect(() => {
    const fetchData = async () => {
      const result = await axios(GET_CITIES);
      setCities(result);
      console.log(result);
    };
    fetchData();
  }, []);

  const handleAdd = () => {
    console.log(input);
    const selected = cities.data.find((c) => c.city === input);
    if(selected){
      addFavorite(selected.city);
    }
    else{
      console.log("could not find city");
    }
};

async function addFavorite(favCity) {
  await axios
    .get(`https://localhost:7238/add/city/${favCity}`)
    .then(() => {
      console.log('Added');
    })
    .catch(() => {
      console.log('error');
    });
}

const handleSelect = (val) => { 
  setInput(val.city);
}

const filteredCities = cities.data.filter(city => city.isFav);
console.log(filteredCities)
 
  return(
    <>
        <Wrapper>
          <Input
            type='text'
            placeholder='search for cities...'
            value={input}
            onChange={e => setInput(e.target.value)}/>
            {cities.data.filter((val)=> {
              if (input == "") {
                return val
              } else if (val.city.toLowerCase().includes(input.toLowerCase())){
                return val
              }
            }).map((val, key) => {
              return (
                <ItemList key={key}>
                <p onClick={() => handleSelect(val)}>{val.city}</p>
                </ItemList>
              );
            })}
          <button onClick={handleAdd}>Add</button>
          <div>
            <h2>Favorite cities:</h2>
            {filteredCities.map((city, index) => (
            <ul key={index}>
              <li>{city.city}</li>
            </ul>))}
          </div>
        </Wrapper>
    </>
  )
}
export default SearchBar