import React, { useState, useEffect } from 'react';
import { getRoutes } from "../API/RoutesApi";
import MyFlightSearchForm from '../components/MyFlightSearchForm'
import MyInfoList from '../components/MyInfoList'
import MyNewsList from '../components/MyNewsList'
import Grid from '@material-ui/core/Grid';
import "../custom.css"

export const Home = () => {
  // const [routes, setRoutes] = useState([]);
  // const mymethod = async () => {
  //   let temp = await getRoutes();
  //   setRoutes(temp);
  // };
  // useEffect(() => {
  //   mymethod();
  // }, [])
    return (
      <div>
        <div style={{ backgroundColor: 'lightsteelblue', alignContent: 'center' }} >
            <MyFlightSearchForm></MyFlightSearchForm>
        </div>
        <Grid container style={{ display: 'flex', justifyContent: 'center' }}>
            <MyInfoList></MyInfoList>
            <MyNewsList></MyNewsList>
        </Grid>
      </div>   
    );
}
