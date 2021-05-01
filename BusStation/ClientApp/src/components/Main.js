import React, { useState, useEffect} from 'react';
import MyFlightSearchForm from '../components/MyFlightSearchForm'
import MyInfoList from '../components/MyInfoList'
import MyNewsList from '../components/MyNewsList'
import Grid from '@material-ui/core/Grid';
import "../custom.css"

export const Main = () => {
    return (
      <div>
        <div style={{ backgroundColor: 'lightsteelblue'}} >
            <MyFlightSearchForm></MyFlightSearchForm>
        </div>
        <Grid container justify="flex-end">
            <MyInfoList></MyInfoList>
            <MyNewsList></MyNewsList>
        </Grid>
      </div>
    );
}