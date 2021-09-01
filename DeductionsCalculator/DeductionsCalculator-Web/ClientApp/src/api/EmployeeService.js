export function addEmployee(employee) {
    return fetch('https://localhost:5001/Deductions/EmployeeCostPreview',
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            redirect: "manual",
            body: JSON.stringify(employee)
        }
    );
}