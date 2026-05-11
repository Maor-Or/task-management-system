import { logout } from "../../features/auth/utils/logout"

import { getCurrentUser } from "../../features/auth/utils/auth"

const Navbar = () => {
    const user = getCurrentUser();

    return (
        <div className="navbar">
            <h2>Task Manager</h2>

            <div>
                <span>{user?.email}</span>
                <button onClick={logout} >Logout</button>
            </div>
        </div>
    );
};

export default Navbar;