import { useState } from "react"
import { useNavigate } from "react-router";
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
import { addPicture } from "../../modules/PictureManager";

export const PictureAddForm = () => {
    const [picture, setPicture] = useState({
        description: "",
        pictureLocation: ""
    });
    const navigate = useNavigate();

    const submitForm = (e) => {
        e.preventDefault();
        addPicture(picture)
        .then((picture) => navigate(`/userPictures`))
        .catch((err) => alert(`An error occurred: ${err.message}`))
    };

    return (
        <Form onSubmit={submitForm}>
            <FormGroup>
                <Label for="pictureLocation">Image Location</Label>
                <Input
                id="pictureLocation"
                type="text"
                onChange={(e) => setPicture({...picture, pictureLocation: e.target.value})}
                />
            </FormGroup>
            <FormGroup>
                <Label for="description">Description</Label>
                <Input
                id="description"
                type="text"
                onChange={(e) => setPicture({...picture, description: e.target.value})}
                />
            </FormGroup>
            <FormGroup>
                <Button>Add Picture</Button>
            </FormGroup>
        </Form>
    )
}