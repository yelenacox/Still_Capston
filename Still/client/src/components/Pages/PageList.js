import { useEffect, useState } from "react";
import { Card } from "reactstrap";
import { Link } from "react-router-dom";
import { getUserPages } from "../../modules/PageManager";


export const PageList = () => {
    const [pages, setPages] = useState([]);

    const getPagesByUser = () => {
        getUserPages().then(pages => setPages(pages));
    }
    console.log("PAGES!!!!!:", pages)
    useEffect(() => {
        getPagesByUser();
    }, []);

    return (
        <div className="page-container">
            {/* <a href="/addPicture" className="btn btn-dark active mb-3" type="button">New Picture</a> */}
            <div className="row justify-content-center">
                {pages.map((p) => {
                    return <Card key={p.Id}>
                        <div className="page">
                            <h2 className="page-title">{p.title}</h2>
                            <div className="page-description">{p.description}</div>
                            <div className="page-pictures">{p.pagePictures.map((p) => <img style={{ width: 250, margin: 20 }} src={p.picture.pictureLocation} />
                            )}</div>
                        </div>
                    </Card>
                })}
            </div>
        </div>
    )
}