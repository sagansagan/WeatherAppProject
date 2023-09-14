import React, {useState, useEffect} from 'react';
import styled from 'styled-components';
import axios from 'axios';

import sun_icon from '../assets/sun_icon.svg';
import drop_icon from '../assets/drop_icon.svg';
import wind_icon from '../assets/wind_icon.svg';

export const CardContainer = styled.div`
    background-color: #113f67;
    color: white;
    border-radius: 50px;
    width: 55vw;
    height: 85vh;
    display: flex;
    flex-direction: column;
    align-items: center;
`;
export const Icon = styled.img`
   height: 65px;
`;
export const WeatherIcon = styled.img`
    height: 250px;
    margin-top: 5%;
`;
export const Wrapper = styled.div`
    margin: auto;
    display: flex;
    flex-wrap: wrap;
`;
export const Element = styled.div`
    margin: 15px;
    display: flex;
    align-items: flex-start;
    justify-content: space-between;
    gap: 10px;
`;
export const Temp = styled.h1`
margin: 0px;
`;

export const GET_WEATHER_DATA = "https://localhost:7238/weather/stockholm";

function Card(props) {
    const { favCity} = props;

    const [data, setData] = useState({weather: []});

    useEffect(() => {
        const fetchData = async () => {
            const result = await axios( GET_WEATHER_DATA );

            console.log(result);
            setData(result.data);
        };

        fetchData();
    }, []);

    return(
        <CardContainer className={favCity}>
           <div className='weather-image'>
            <WeatherIcon src={sun_icon} alt="sun icon"/>
           </div>
           <Temp>{data.temperature}°C</Temp>
           <h2>{data.city}</h2>
           <Wrapper>
            <Element>
                <Icon src={drop_icon} alt='humidity icon'/>
                <div className='data'>
                    <div>{data.humidity}%</div>
                    <div>Humidity</div>
                </div>
            </Element>
            <Element>
                <Icon src={wind_icon} alt='wind icon'/>
                <div className='data'>
                    <div>{data.wind} km/h</div>
                    <div>Wind speed</div>
                </div>
            </Element>
           </Wrapper>
        </CardContainer>
    );
}

export default Card
