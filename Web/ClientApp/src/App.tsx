import * as React from 'react';
import { Route } from 'react-router';
import Home from './components/Home';

import '../src/components/Site.css'
import Layout from './components/Layout';

export default () => (
    <Layout>
        <Home/>
    </Layout>
);
