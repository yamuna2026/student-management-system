import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { ThemeContext } from "../context/ThemeContext";
import "./Navbar.css";

const Navbar = ({ title }) => {
  const { logout } = useContext(AuthContext);
  const { theme, toggleTheme } = useContext(ThemeContext);

  return (
    <header className="navbar">

      <div className="nav-left">
        <h2 className="logo">{title}</h2>
      </div>

      <div className="nav-right">
        <button className="theme-btn" onClick={toggleTheme}>
          {theme === "dark" ? "🌙" : "☀️"}
        </button>

        <button className="logout-btn" onClick={logout}>
          Logout
        </button>
      </div>

    </header>
  );
};

export default Navbar;