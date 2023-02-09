import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Card } from "reactstrap";
import { getAllPictures } from "../../modules/PictureManager";

export const PictureList = () => {
    const [pictures, setPictures] = useState([]);
    
    const getAll = () => {
        getAllPictures().then(pictures => setPictures(pictures));
    }

    useEffect(() => {
        getAll();
    }, []);

    return (
        <div className="picture-container">
            <div className="row justify-content-center">
                {pictures.map((p) => {return <Card key={p.Id}>
                    <div className="picture"><img style={{ width: 300 }}src={p.pictureLocation} /></div>
                     </Card>})}
            </div>
        </div>
    )
}