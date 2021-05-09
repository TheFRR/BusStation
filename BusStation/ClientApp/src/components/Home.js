import React from 'react';
import MyFlightSearchForm from '../components/MyFlightSearchForm'
import MyInfoList from '../components/MyInfoList'
import MyNewsList from '../components/MyNewsList'
import Grid from '@material-ui/core/Grid';
import "../custom.css"

export const Home = () => {
  localStorage.clear();
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
