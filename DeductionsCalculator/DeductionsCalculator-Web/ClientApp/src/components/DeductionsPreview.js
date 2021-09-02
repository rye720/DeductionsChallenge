import React, { Component } from 'react';

export class DeductionsPreview extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return this.props.isLoaded && this.props.deductionsPreview && (
            <div>
                <ul>
                    <li>Total Cost: {this.props.deductionsPreview.totalCost}</li>
                    <li>Employee Cost: {this.props.deductionsPreview.employeeCost}</li>
                    <li>Dependents Cost: {this.props.deductionsPreview.dependentsCost}</li>
                </ul>
                <div>
                    Employee Name : {this.props.deductionsPreview.employee.name}

                    {this.props.deductionsPreview.employee.dependents.map(dependent =>
                        <li key={dependent.id}>
                            {dependent.name}
                        </li>
                    )}
                </div>
            </div>
        )
    }
}
