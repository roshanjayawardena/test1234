import React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";

const AdminOnlyRoute = ({ children }) => {

 const user = useSelector((state) => state.auth.user);

  if (user && user.role === "Admin") {
    return children;
  }
  return (
    <section style={{ height: "80vh" }}>
      <div className="container">
        <h2>Permission Denied.</h2>
        <p>This page can only be view by an Admin user.</p>
        <br />
        <Link to="/">
          <button className="--btn">&larr; Back To Home</button>
        </Link>
      </div>
    </section>
  );
};

export const AdminOnlyLink = ({ children }) => {

 const user = useSelector((state) => state.auth.user);

  if (user && user.role === "Admin") {
    return children;
  }
  return null;
};

export default AdminOnlyRoute;