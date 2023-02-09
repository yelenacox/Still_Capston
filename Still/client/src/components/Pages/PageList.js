import { useEffect, useState } from "react";
import { Card } from "reactstrap";
import { Link } from "react-router-dom";
import { getUserPages } from "../../modules/PageManager";
import "./PageDetails.css"


export const PageList = () => {
    const [pages, setPages] = useState([]);

    const getPagesByUser = () => {
        getUserPages().then(pages => setPages(pages));
    }
    
    useEffect(() => {
        getPagesByUser();
    }, []);

    return (
        <div className="page-container">
            {/* <a href="/addPicture" className="btn btn-dark active mb-3" type="button">New Picture</a> */}
            <div className="row justify-content-center">
                {pages.map((p) => {
                    return <Card key={p.Id}>
                        <Link to={`/page/${p.id}`} style={{textDecoration: 'none', color: 'inherit'}}>
                            <div className="page">
                            <h4 className="page-title">{p.title}</h4>
                            {p.description}
                            {/* <div className="page-description">{p.description}</div>
                            <div className="page-detail-pictures" style={{display: 'flex', flexFlow: 'row wrap', justifyContent: 'center'}}>
                            {p.pagePictures?.map((p) => {return <>
                            <div className="picture-with-description" style={{display: 'flex', flexDirection: 'column'}}>
                                <img style={{ width: 50, margin: 5 }}src={p.picture.pictureLocation} />
                        <p style={{fontSize: 5}}>{p.picture.description}</p>
                        </div>
                            </>}
                        )}</div> */}

                           
                        </div></Link>
                    </Card>
                })}
            </div>
        </div>
    )
}