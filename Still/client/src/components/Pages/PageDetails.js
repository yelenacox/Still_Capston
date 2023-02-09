import { useEffect, useState } from "react"
import { useParams } from "react-router"
import { Button, Card, Input } from "reactstrap"
import { getPageById } from "../../modules/PageManager"
import "./PageDetails.css"


export const PageDetails = () => {
    const {pageId} = useParams()
    const [page, setPage] = useState ({})

    const getPageDetails = () => {
        getPageById(pageId).then(page => setPage(page));
    }

    useEffect(() => {
        getPageDetails();
    }, [pageId]);

    return (
        <div className="page-container">
            <div>
                <Card>
                    <div className="page-detail" style={{border: '1px solid black', margin: 20}}>
                        <h2 className="page-detail-title">{page.title}</h2>
                        <div className="page-detail-description">{page.description}</div>
                        <div className="page-detail-pictures" style={{display: 'flex', flexFlow: 'row wrap', justifyContent: 'center'}}>
                            {page.pagePictures?.map((p) => {return <>
                            <div className="picture-with-description" style={{display: 'flex', flexDirection: 'column'}}>
                                <img style={{ width: 200, margin: 15 }}src={p.picture.pictureLocation} />
                        <p>{p.picture.description}</p>
                        </div>
                            </>}
                        )}</div>
                    </div>
                </Card>
            </div>
        </div>
    )
}