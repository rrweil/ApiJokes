import React, { Component } from 'react';
import { Route } from 'react-router-dom';
import Layout from './Layout';
import { AuthContextComponent } from './AuthContext';
import Home from './Pages/home';
import ViewAll from './Pages/viewAll';
import Signup from './Pages/Signup';
import Logout from './Pages/Logout';
import Login from './Pages/Login';


export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <AuthContextComponent>
        <Layout>
          <Route exact path='/' component={Home} />
          <Route exact path='/viewall' component={ViewAll} />
          <Route exact path='/signup' component={Signup} />
          <Route exact path='/login' component={Login} />
          <Route exact path='/logout' component={Logout} />
        </Layout>
      </AuthContextComponent>
    );
  }
}