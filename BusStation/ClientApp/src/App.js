import React, { Component } from 'react';
import {Route, Switch} from 'react-router'
import { Home } from './components/Home';
import { Login } from './components/Login'
import { Register } from './components/Register'
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
      </Switch>  
    </>
    )
  }
}
