import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Hello from "./Hello";
import Login from "./Login";
import { PageList } from "./Pages/PageList";
import { PictureAddForm } from "./Pictures/PictureAddForm";
import { PictureDetails } from "./Pictures/PictureDetails";
import { PictureList } from "./Pictures/PictureList";
import { UserPictureList } from "./Pictures/UserPictureList";
import Register from "./Register";
import { UserList } from "./UserList";

export default function ApplicationViews({ isLoggedIn }) {
  return (
    <main>
      <Routes>
        <Route path="/">

          <Route
            index
            element={isLoggedIn ? <Hello /> : <Navigate to="/login" />}
          />
          <Route
            path="UserList"
            element={isLoggedIn ? <UserList /> : <Navigate to="/UserList" />}
          />
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="userpictures" element={<UserPictureList />} />
          <Route path="picture/:picId" element={<PictureDetails />} />
          <Route path="addPicture" element={<PictureAddForm />} />
          <Route path="page" element={<PageList />} />

postId
          <Route path="*" element={<p>Whoops, nothing here...</p>} />
        </Route>
      </Routes>
    </main>
  );
};
