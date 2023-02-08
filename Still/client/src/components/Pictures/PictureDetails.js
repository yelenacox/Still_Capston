import { useEffect, useState } from "react"
import { useParams } from "react-router"
import { Button, Card, Input } from "reactstrap"
import { editPicture, getPicById } from "../../modules/PictureManager"

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
                <Card key={pic.Id}>
                    <div className="picture"><img style={{ width: 500 }} src={pic.pictureLocation} /></div>
                    {!picEdit ? <>
                    <div className="picture-description">{pic.description} </div> 
                    <Button onClick={() => setPicEdit(true)}>Edit</Button></>
                    : <><div clasName="picture-descrption-edit">
                        <Input 
                        type="text"
                        placeholder="picture-description"
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