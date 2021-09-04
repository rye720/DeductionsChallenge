import React, { Component } from 'react';
import { Card, Divider, Typography, CardContent } from '@material-ui/core';

export class Header extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <Card>
                    <CardContent className="header">
                        <Typography variant="h4" component="h4">
                            Deductions Preview Calculator
                        </Typography>
                        <Typography variant="subtitle1" component="div">
                            Enter Employee and Dependent names to get a preview of the costs.
                        </Typography>
                    </CardContent>
                </Card>
                <Divider />
            </div>
        )
    }
}
