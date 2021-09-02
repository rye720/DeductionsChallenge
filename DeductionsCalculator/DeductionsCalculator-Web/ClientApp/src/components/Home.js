import React, { Component } from 'react';
import { EmployeeForm } from './EmployeeForm';
import { DeductionsPreview } from './DeductionsPreview';

export class Home extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            deductionsPreview: null,
            isLoaded: false
        }

        this.updateDeductionsPreview = this.updateDeductionsPreview.bind(this);
    }

    updateDeductionsPreview(deductionsPreview) {
        this.setState({ deductionsPreview, isLoaded: true })
    }

    render() {
        return (
            <div>
                <EmployeeForm {...this.state} updateDeductionsPreview={this.updateDeductionsPreview} />
                <DeductionsPreview {...this.state} />
            </div>
        );
    }
}
