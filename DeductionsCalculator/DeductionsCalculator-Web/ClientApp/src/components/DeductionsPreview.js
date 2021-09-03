import React, { Component } from 'react';
import { Avatar, Divider, List, ListItem } from '@material-ui/core';

export class DeductionsPreview extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return this.props.isLoaded && this.props.deductionsPreview && (
            <div>
                <br />
                <Divider />
                <div className="employee-result">
                    Employee Name : {this.props.deductionsPreview.employee.name}
                    <List>
                        {this.props.deductionsPreview.employee.dependents.map(dependent =>
                            <ListItem key={dependent.id}>
                                <Avatar className="small-avatar" />
                                {dependent.name}
                            </ListItem>
                        )}
                    </List>
                </div>
                <div>
                    <List>
                        <ListItem>Total Cost: ${this.props.deductionsPreview.totalCost}</ListItem>
                        <ListItem>Employee Cost: ${this.props.deductionsPreview.employeeCost}</ListItem>
                        <ListItem>Dependents Cost: ${this.props.deductionsPreview.dependentsCost}</ListItem>
                    </List>
                </div>
            </div>
        )
    }
}
