import { useState, useContext } from "react";
import API from "../services/api";
import { AuthContext } from "../context/AuthContext";
import "./Home.css";

const Home = () => {
  const { login } = useContext(AuthContext);

  const [admin, setAdmin] = useState({ username: "", password: "" });
  const [user, setUser] = useState({ username: "", password: "" });
  const [msg, setMsg] = useState("");

  const show = (m) => {
    setMsg(m);
    setTimeout(() => setMsg(""), 3000);
  };

  const handle = async (form, role, type) => {
    try {
      const url = type === "login" ? "/auth/login" : "/auth/register";

      const res = await API.post(url, { ...form, role });

      if (type === "login") {
        login(res.data.token, role);
        window.location.href = role === "Admin" ? "/admin" : "/user";
      } else {
        show(res.data.message);
      }
    } catch {
      show("Error occurred");
    }
  };

  return (
    <div className="home-bg">
      {msg && <div className="toast">{msg}</div>}

      <h1 className="home-title">Student Management System</h1>

      <div className="home-grid">

        {/* ADMIN */}
        <div className="glass">
          <h2>Admin</h2>
          <input placeholder="Username"
            onChange={(e) => setAdmin({ ...admin, username: e.target.value })}/>
          <input type="password" placeholder="Password"
            onChange={(e) => setAdmin({ ...admin, password: e.target.value })}/>
          
          <button onClick={() => handle(admin, "Admin", "register")}>Register</button>
          <button onClick={() => handle(admin, "Admin", "login")}>Login</button>
        </div>

        {/* USER */}
        <div className="glass">
          <h2>User</h2>
          <input placeholder="Username"
            onChange={(e) => setUser({ ...user, username: e.target.value })}/>
          <input type="password" placeholder="Password"
            onChange={(e) => setUser({ ...user, password: e.target.value })}/>

          <button onClick={() => handle(user, "User", "register")}>Register</button>
          <button onClick={() => handle(user, "User", "login")}>Login</button>
        </div>

      </div>
    </div>
  );
};

export default Home;