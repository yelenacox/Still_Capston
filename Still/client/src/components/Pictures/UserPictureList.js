import firebase from "firebase/app";
import "firebase/auth";
import { useEffect, useState } from "react";
import { getUserPictures } from "../../modules/PictureManager";
import { Card } from "reactstrap";


export const UserPictureList = () => {
    const [userPictures, setUserPictures] = useState ([]);

    const firebaseUserId = firebase.auth().currentUser.uid

    const getPicturesByUser = () => {
        getUserPictures(firebaseUserId).then(userPictures => setUserPictures(userPictures));
    }

    useEffect(() => {
        getPicturesByUser();
    }, []);

    return (
        <div className="picture-container">
            <div className="row justify-content-center">
                {userPictures.map((p) => {return <Card key={p.Id}>
                    <div className="picture"><img style={{ width: 300 }} src={p.pictureLocation} /></div>
                    <div className="picture-description">{p.description}</div>
                </Card>})}
            </div>
        </div>
    )
}