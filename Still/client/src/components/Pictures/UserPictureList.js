import firebase from "firebase/app";
import "firebase/auth";
import { useEffect, useState } from "react";
import { getUserPictures } from "../../modules/PictureManager";
import { Card } from "reactstrap";
import { Link } from "react-router-dom";


export const UserPictureList = () => {
    const [userPictures, setUserPictures] = useState ([]);

    const firebaseUserId = firebase.auth().currentUser.uid

    const getPicturesByUser = () => {
        getUserPictures().then(userPictures => setUserPictures(userPictures));
    }

    useEffect(() => {
        getPicturesByUser();
    }, []);

    return (
        <div className="picture-container">
            <div className="row justify-content-center">
                {userPictures.map((p) => {return <Card key={p.Id}>
                    <div className="picture"><Link to={`/picture/${p.id}`}><img style={{ width: 300 }} src={p.pictureLocation} /></Link></div>
                </Card>})}
            </div>
        </div>
    )
}