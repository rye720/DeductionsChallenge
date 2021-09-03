import React, { Component } from 'react';
import { Header } from './Header';
import { EmployeeForm } from './EmployeeForm';
import { DeductionsPreview } from './DeductionsPreview';

export class Home extends Component {
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
                <Header />
                <EmployeeForm className="centered-container" {...this.state} updateDeductionsPreview={this.updateDeductionsPreview} />
                <DeductionsPreview className="centered-container" {...this.state} />
            </div>
        );
    }
}
