import React from 'react';
import { Route } from 'react-router';
import { Container } from 'reactstrap';
import Growth from './Growth'

export default () => (
    <Container>
        <Route exact path='/' component={Growth} />
    </Container>
);
