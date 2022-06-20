import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './ImageContainer.css';
import designImage from './designDiagram.png';

export class ImageContainer extends Component {
    static displayName = ImageContainer.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-lg " light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">Image</NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="sm" />
                        <Collapse className="d-lg-inline-flex flex-lg-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                              
                                <NavItem><img height = "100" src={designImage} /></NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
