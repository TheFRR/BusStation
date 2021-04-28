import React, { useState, useEffect} from 'react';
import { getRoutes } from "../API/routesApi";

export const Home = () => {
  const [routes, setRoutes] = useState([]);
  const mymethod = async () => {
    let temp = await getRoutes();
    setRoutes(temp);
  };
  useEffect(() => {
    mymethod();
  }, [])
    return (
      <div>
        {
          routes.map((route) => (
            <div>
              <p>Номер: {route.id}</p>
              <p>Пункт отправления: {route.departure}</p> 
              <p>Пункт прибытия: {route.arrival}</p> 
              <p>Время в пути: {route.travelTime}</p> 
              <br></br>
            </div>
          ))
        }
      </div>
    );
}
