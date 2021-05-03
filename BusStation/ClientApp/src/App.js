import React, { Component } from 'react';
import {Route, Switch} from 'react-router'
import { Home } from './components/Home';
import { Login } from './components/Login'
import { Register } from './components/Register'
import { UpdateRoute } from './components/UpdateRoute'
import { BuyTicket } from './components/BuyTicket'
import { UpdateFlight } from './components/UpdateFlight'
import MyAppBar from "./components/MyAppBar"

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <>
      <MyAppBar></MyAppBar>  
      <Switch>
        <Route exact path='/' component={Home} />
        <Route path='/login' component={Login} />
        <Route path='/register' component={Register} />
        <Route path='/route' component={UpdateRoute} />
        <Route path='/buy' component={BuyTicket} />
        <Route path='/flight' component={UpdateFlight} />
      </Switch>  
    </>
    )
  }
}
