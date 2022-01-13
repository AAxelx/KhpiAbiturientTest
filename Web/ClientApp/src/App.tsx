import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';

import './Site.css'
import { actionCreators } from './store/SiteStore';

export default () => (
    <Layout>
        <Route exact path='/'><Home getQuestion={actionCreators.getQuestion} sendAnswers={actionCreators.sendAnswers} sendEmail={actionCreators.sendEmail} editAnswers={actionCreators.editAnswers} /></Route>
    </Layout>
);
