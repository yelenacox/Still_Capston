import { useState, useEffect } from "react"
import { useNavigate, useParams } from "react-router";
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
import { addPage } from "../../modules/PageManager";
import { getUserPictures } from "../../modules/PictureManager";

export const PageAddForm = () => {
    const [page, setPage] = useState({
        title: "",
        description: ""
    });
    const [pictures, setPictures] = useState([]);
    const {pageId} = useParams() 
    const getAll = () => {
        getUserPictures().then(pictures => setPictures(pictures));
    }

    useEffect(() => {
        getAll();
    }, []);


    const navigate = useNavigate();

    const submitForm = (e) => {
        e.preventDefault();
        const picIds = []
        e.target.pagePicture.forEach((picture) => {
            if (picture.checked) { picIds.push(picture.id) }
        })
        addPage(page, picIds)
        .then(() => navigate(`/page`))
        .catch((err) => alert(`An error occurred: ${err.message}`))
    };

    return (
        <Form onSubmit={submitForm}>
            <FormGroup>
                <Label for="title">Title</Label>
                <Input
                    id="title"
                    type="text"
                    onChange={(e) => setPage({ ...page, title: e.target.value })}
                />
            </FormGroup>
            <FormGroup>
                <Label for="description">Description</Label>
                <Input
                    id="description"
                    type="textarea"
                    onChange={(e) => setPage({ ...page, description: e.target.value })}
                />
            </FormGroup>
            <FormGroup check>
                <Label for="pagePicture">Pictures</Label>
                {pictures.map((p) => {
                    return <><Input
                        type="checkbox"
                        id={p.id}
                        name="pagePicture"
                    />
                        <Label check><img style={{ width: 100 }} src={p.pictureLocation} /></Label></>
                })}
            </FormGroup>
            <FormGroup>
                <Button>Add Page</Button>
            </FormGroup>
        </Form>
    )
}