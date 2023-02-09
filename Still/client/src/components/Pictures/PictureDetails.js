import { useEffect, useState } from "react"
import { useParams } from "react-router"
import { Button, Card, Input } from "reactstrap"
import { editPicture, getPicById } from "../../modules/PictureManager"
import { PictureDelete } from "./PictureDelete"
import "./PictureDetails.css"

export const PictureDetails = () => {
    const {picId} = useParams()
    const [pic, setPic] = useState ({})
    const [description, setDescription] = useState();
    const [picEdit, setPicEdit] = useState(false);

    const getPicDetails = () => {
        getPicById(picId).then(pic => {setPic(pic); setDescription(pic.description)});
    }

    useEffect(() => {
        getPicDetails();
    }, [picId]);

    const handleSaveButtonClick=()=>{
        editPicture({...pic, description:description})
        getPicDetails()
        setPicEdit(false)
    }
    return (
        <div className="picture-container">
            <div className="row justify-content-center">
                <Card key={pic.id}>
                    <div className="picture"><img style={{ width: 500 }} src={pic.pictureLocation} /></div>
                    {!picEdit ? <>
                    <div className="picture-description">{pic.description} </div> 
                    <PictureDelete/>
                    <Button onClick={() => setPicEdit(true)}>Edit</Button></>
                    : <><div clasName="picture-description-edit">
                        <Input 
                        type="text"
                        placeholder="Picture description"
                        value={description}
                        onChange={(e)=>{setDescription(e.target.value)}}
                        />
                        </div>
                    <Button onClick={handleSaveButtonClick}>Save</Button><Button onClick={() => setPicEdit(false)}>Cancel</Button>
                    </>}
                    <div className="picture-date">{pic.dateCreated}</div>
                </Card>
            </div>
        </div>
    )
}