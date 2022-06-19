import React, { Component } from 'react';


import ErrorPage400 from './ErrorPages/Error400.js';
import ErrorPage404 from './ErrorPages/Error404.js';
import ErrorPage500 from './ErrorPages/Error500.js';
export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { users: [], responseCode: "", responseDescription: "", loading: true, loadTime: 0 };
    }

    componentDidMount() {
        this.populateDataTable();
    }

    static renderDataTable(users, loadTime) {
        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email</th>
                            <th>IP Address</th>
                            <th>Location</th>
                            <th>Latitude</th>
                            <th>Longtitude</th>
                        </tr>
                    </thead>
                    <tbody>
                        {users.map(user =>
                            <tr key={user.id}>
                                <td>{user.id}</td>
                                <td>{user.firstName}</td>
                                <td>{user.lastName}</td>
                                <td>{user.email}</td>
                                <td>{user.ipAddress}</td>
                                <td>{user.city}</td>
                                <td>{user.latitude}</td>
                                <td>{user.longitude}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <div>
                    <p>Time taken to find and load {users.length} matching records: {loadTime} seconds.</p>
                </div>
            </div>
        );
    }

    render() {
        if (this.state.responseCode === "200") {
            console.log(this.state.responseCode);
       // if (this.state.users.length > 0) {
            let contents = this.state.loading
                ? <p><em>Loading...</em></p>
                : FetchData.renderDataTable(this.state.users, this.state.loadTime);
            return (
                <div>
                    <h1 id="tabelLabel" >Answer.</h1>
                    <p>List of people who are listed as either living in London, or whose current coordinates are within 50 miles of London.</p>
                    <p>This component demonstrates fetching data from the server using .NET 6 HttpClientFactory server-side API calls.</p>
                    {contents}
                </div>
            );

        }
        else if (this.state.responseCode === "400") {
            return <p><ErrorPage400 /></p>;
        }
        else if (this.state.responseCode === "404") {
            return <p><ErrorPage404 /></p>;
        }
        else if (this.state.responseCode === "500") {
            return <p><ErrorPage500/></p>;
        }
        else {
            console.log(this.state.responseCode);
            return <p><em>Loading...</em></p>;
        }
    }

    async populateDataTable() {
        const startDateAndTime = new Date();
        const response = await fetch('getusers?city=London');
        const data = await response.json();
        const timeTaken = ((new Date()).getTime() - startDateAndTime.getTime()) / 1000;
       console.log(data);
        this.setState({ users: data.userList, responseCode: data.responseCode, responseDescription: data.responseDescription, loading: false, loadTime: timeTaken });
    }
}
