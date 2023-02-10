import { useNavigate, useParams } from "react-router"
import { Button } from "reactstrap"
import { deletePage } from "../../modules/PageManager";

export const PageDelete = () => {
    const {pageId} = useParams();

    const navigate = useNavigate();
    
    const handleSaveButtonClick = () => {
        deletePage(pageId)
        .then(() => {
            navigate("/page")
        })
    }

   return <Button
    onClick={handleSaveButtonClick}>Delete</Button>
    }
