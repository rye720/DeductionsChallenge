import React, { Component } from 'react';
import { addEmployee } from '../api/EmployeeService';
import { Employee } from '../models/Employee';
import { Dependent } from '../models/Dependent';

export class EmployeeForm extends React.Component {
    getInitialState = () => {
        return {
            employeeName: '',
            dependentName: '',
            dependents: [],
            items: '',
            isLoaded: false
        }
    };

    constructor(props) {
        super(props);

        this.state = this.getInitialState();

        this.addDependentToList = this.addDependentToList.bind(this);
        this.handleEmployeeChange = this.handleEmployeeChange.bind(this);
        this.handleDependentChange = this.handleDependentChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    addDependentToList() {
        this.setState({
            dependentName: '',
            dependents: [...this.state.dependents, this.state.dependentName] //"spread syntax"
        });
    }

    handleEmployeeChange(event) {
        this.setState({
            employeeName: event.target.value
        });
    }

    handleDependentChange(event) {
        this.setState({
            dependentName: event.target.value
        });
    }

    handleSubmit(event) {
        let dependents = this.state.dependents.map(name => new Dependent(name));
        let employee = new Employee(this.state.employeeName);
        employee.dependents = dependents;
        addEmployee(employee)
            .then(response => response.json())
            .then(response => {
                console.log('Success:', response)
            });

        event.preventDefault();

        this.setState(this.getInitialState());
    }

    render() {
        console.log(this.props);
        return (
            <form onSubmit={this.handleSubmit}>
                <label>Employee name:</label>
                <input type="text" value={this.state.employeeName} onChange={this.handleEmployeeChange} />
                <input type="submit" value="Submit" />
                <br />
                <label>Dependent name:
                <input type="text" value={this.state.dependentName} onChange={this.handleDependentChange} />
                </label>
                <button type="button" onClick={this.addDependentToList}>Add</button>

            </form>



        );
    }
}
