import { useNavigate, useParams } from "react-router"
import { Button } from "reactstrap"
import { deletePicture, getAllPictures } from "../../modules/PictureManager"

export const PictureDelete = () => {
    const {picId} = useParams();

    const navigate = useNavigate();
    
    const handleSaveButtonClick = () => {
        deletePicture(picId)
        .then(() => {
            navigate("/UserPictures")
        })
    }

   return <Button
    onClick={handleSaveButtonClick}>Delete</Button>
    }
