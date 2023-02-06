import { useEffect, useState } from "react"
import { useParams } from "react-router"
import { Card } from "reactstrap"
import { getPicById } from "../../modules/PictureManager"

export const PictureDetails = () => {
    const {picId} = useParams()
    const [pic, setPic] = useState ({})

    const getPicDetails = () => {
        getPicById(picId).then(pic => setPic(pic));
    }

    useEffect(() => {
        getPicDetails();
    }, [picId]);

    return (
        <div className="picture-container">
            <div className="row justify-content-center">
                <Card key={pic.Id}>
                    <div className="picture"><img style={{ width: 500 }} src={pic.pictureLocation} /></div>
                    <div className="picture-description">{pic.description} </div>
                    <div className="picture-date">{pic.dateCreated}</div>
                </Card>
            </div>
        </div>
    )
}