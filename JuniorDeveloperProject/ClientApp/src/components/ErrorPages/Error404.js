import { Component } from "react"

class ErrorPage404 extends Component {
    render() {
        return (

            <div >
                <div class="alert alert-danger" role="alert">
                    The website you were trying to reach couldn't be found on the server. Please try again later!
                </div>
            </div>
        )
    }
}
export default ErrorPage404;