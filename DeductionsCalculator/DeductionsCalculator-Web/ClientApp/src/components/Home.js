import React, { Component } from 'react';
import { EmployeeForm } from './EmployeeForm';

export class Home extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <EmployeeForm />
            </div>
        );
    }

}
