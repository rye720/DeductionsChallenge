import React, { Component } from 'react';
import { addEmployee } from '../api/EmployeeService';
import { Employee } from '../models/Employee';
import { Dependent } from '../models/Dependent';
import { Avatar, Button, TextField, List, ListItem, Divider } from '@material-ui/core';



export class EmployeeForm extends Component {

    validateInputRegex = /^[A-Za-z][A-Za-z ]*$/;

    clearForm = () => {
        return {
            employeeName: '',
            dependentName: '',
            dependents: []
        }
    };

    constructor(props) {
        super(props);

        this.state = this.clearForm();

        this.addDependentToList = this.addDependentToList.bind(this);
        this.handleEmployeeChange = this.handleEmployeeChange.bind(this);
        this.handleDependentChange = this.handleDependentChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    addDependentToList() {
        this.setState({
            dependentName: '',
            dependents: [...this.state.dependents, this.state.dependentName]
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
            .then(
                (result) => {
                    this.props.updateDeductionsPreview(result);

                    //console.log('Success:', result);
                });

        event.preventDefault();

        this.setState(this.clearForm());
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>

                <TextField type="text" label="Employee name:" value={this.state.employeeName}
                    error={this.state.employeeName && !this.state.employeeName.match(this.validateInputRegex)} onChange={this.handleEmployeeChange}

                    helperText={this.state.employeeName && !this.state.employeeName.match(this.validateInputRegex) && 'Name must begin with a letter and not contain numbers or special characters'} />

                <br />

                <div>
                    <TextField type="text" label="Dependent name:" value={this.state.dependentName}
                        error={this.state.dependentName && !this.state.dependentName.match(this.validateInputRegex)} onChange={this.handleDependentChange}
                        helperText={this.state.dependentName && !this.state.dependentName.match(this.validateInputRegex) && 'Name must begin with a letter and not contain numbers or special characters'} />

                    <Button className="add-button" disabled={!this.state.dependentName.match(this.validateInputRegex)}
                        type="submit" onClick={this.addDependentToList} variant="contained" size="large">Add</Button>
                </div>
                <List>
                    {
                        this.state.dependents.map(dependent =>
                            <ListItem>
                                <Avatar className="small-avatar" />{dependent}
                            </ListItem>
                        )
                    }
                </List>

                <Button disabled={!this.state.employeeName.match(this.validateInputRegex)}
                    type="submit" variant="outlined" size="large">Get Costs Preview</Button>
            </form>
        );
    }
}

