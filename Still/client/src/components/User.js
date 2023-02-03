import React from "react";
import { Card, CardBody } from "reactstrap";

export default function User({ user }) {
    return (
        <Card className="m-4">
            <CardBody>
                <p>{user.name}</p>
                <p>{user.email}</p>
            </CardBody>
        </Card>
    );
}
