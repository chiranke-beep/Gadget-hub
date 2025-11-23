import React, { useState, useRef, useEffect } from 'react';
import { Link, useLocation } from 'react-router-dom';
import Icon from '../AppIcon';
import Button from './Button';
import Input from './Input';

const Header = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [isUserMenuOpen, setIsUserMenuOpen] = useState(false);
  const [isSearchOpen, setIsSearchOpen] = useState(false);
  const [searchQuery, setSearchQuery] = useState('');
  const [cartCount, setCartCount] = useState(3);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  
  const location = useLocation();
  const userMenuRef = useRef(null);
  const searchRef = useRef(null);

  const navigationItems = [
    { label: 'Home', path: '/home-page', icon: 'Home' },
    { label: 'Products', path: '/product-catalog', icon: 'Package' },
    { label: 'Cart', path: '/shopping-cart', icon: 'ShoppingCart', badge: cartCount }
  ];

  // Helper to navigate to dashboard with section
  const navigateToDashboardSection = (section) => {
    window.location.href = `/customer-dashboard#${section}`;
  };

  const userMenuItems = isAuthenticated ? [
    { label: 'Dashboard', path: '/customer-dashboard', icon: 'LayoutDashboard' },
    { label: 'Profile', onClick: () => navigateToDashboardSection('profile'), icon: 'User' },
    { label: 'Orders', onClick: () => navigateToDashboardSection('orders'), icon: 'Package' },
    { label: 'Settings', onClick: () => navigateToDashboardSection('settings'), icon: 'Settings' },
    { label: 'Logout', action: 'logout', icon: 'LogOut' }
  ] : [
    { label: 'Login', path: '/login-screen', icon: 'LogIn' },
    { label: 'Register', path: '/registration-screen', icon: 'UserPlus' }
  ];

  useEffect(() => {
    const checkAuth = () => {
      const token = localStorage.getItem('authToken');
      const user = localStorage.getItem('user');
      setIsAuthenticated(!!token || !!user);
    };
    checkAuth();
    window.addEventListener('storage', checkAuth);
    return () => window.removeEventListener('storage', checkAuth);
  }, []);

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (userMenuRef.current && !userMenuRef.current.contains(event.target)) {
        setIsUserMenuOpen(false);
      }
      if (searchRef.current && !searchRef.current.contains(event.target)) {
        setIsSearchOpen(false);
      }
    };

    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  const handleSearch = (e) => {
    e.preventDefault();
    if (searchQuery.trim()) {
      console.log('Searching for:', searchQuery);
      setIsSearchOpen(false);
    }
  };

  const handleUserAction = (action) => {
    if (action === 'logout') {
      // Remove user data from localStorage
      localStorage.removeItem('user');
      localStorage.removeItem('authToken');
      localStorage.removeItem('userEmail');
      localStorage.removeItem('userName');
      localStorage.removeItem('isAuthenticated');
      setIsAuthenticated(false);
      setIsUserMenuOpen(false);
      window.location.href = '/login-screen';
    }
  };

  const isActivePath = (path) => {
    return location.pathname === path;
  };

  return (
    <header className="fixed top-0 left-0 right-0 z-1000 bg-background border-b border-border">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          {/* Logo */}
          <div className="flex-shrink-0">
            <Link 
              to="/home-page" 
              className="flex items-center space-x-2 hover:opacity-80 transition-opacity duration-150"
            >
              <span className="text-xl font-bold text-foreground font-sans">
                GadgetHub
              </span>
            </Link>
          </div>

          {/* Desktop Navigation */}
          <nav className="hidden md:flex items-center space-x-8">
            {navigationItems.slice(0, 2).map((item) => (
              <Link
                key={item.path}
                to={item.path}
                className={`flex items-center space-x-2 px-3 py-2 rounded-lg text-sm font-medium transition-all duration-150 hover:bg-muted hover:transform hover:-translate-y-0.5 ${
                  isActivePath(item.path)
                    ? 'text-primary bg-muted' :'text-muted-foreground hover:text-foreground'
                }`}
              >
                <Icon name={item.icon} size={18} />
                <span>{item.label}</span>
              </Link>
            ))}
          </nav>

          {/* Search Bar - Desktop */}
          <div className="hidden md:flex flex-1 max-w-md mx-8" ref={searchRef}>
            <form onSubmit={handleSearch} className="w-full relative">
              <Input
                type="search"
                placeholder="Search products..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="w-full pl-10 pr-4"
              />
              <Icon 
                name="Search" 
                size={18} 
                className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground pointer-events-none" 
              />
            </form>
          </div>

          {/* Right Section */}
          <div className="flex items-center space-x-4">
            {/* Search Icon - Mobile */}
            <button
              onClick={() => setIsSearchOpen(!isSearchOpen)}
              className="md:hidden p-2 rounded-lg text-muted-foreground hover:text-foreground hover:bg-muted transition-all duration-150"
            >
              <Icon name="Search" size={20} />
            </button>

            {/* Cart */}
            <Link
              to="/shopping-cart"
              className={`relative p-2 rounded-lg transition-all duration-150 hover:bg-muted hover:transform hover:-translate-y-0.5 ${
                isActivePath('/shopping-cart')
                  ? 'text-primary bg-muted' :'text-muted-foreground hover:text-foreground'
              }`}
            >
              <Icon name="ShoppingCart" size={20} />
              {cartCount > 0 && (
                <span className="absolute -top-1 -right-1 bg-accent text-accent-foreground text-xs font-medium rounded-full h-5 w-5 flex items-center justify-center animate-pulse-slow">
                  {cartCount}
                </span>
              )}
            </Link>

            {/* User Menu */}
            <div className="relative flex items-center" ref={userMenuRef}>
              <button
                onClick={() => setIsUserMenuOpen(!isUserMenuOpen)}
                className="flex items-center space-x-2 p-2 rounded-lg text-muted-foreground hover:text-foreground hover:bg-muted transition-all duration-150"
              >
                <Icon name={isAuthenticated ? "User" : "UserCircle"} size={20} />
                <Icon name="ChevronDown" size={16} className={`transform transition-transform duration-150 ${isUserMenuOpen ? 'rotate-180' : ''}`} />
              </button>

              {/* Visible Logout Button */}
              {isAuthenticated && (
                <Button
                  variant="outline"
                  size="sm"
                  className="ml-2"
                  onClick={() => handleUserAction('logout')}
                  iconName="LogOut"
                  iconPosition="left"
                >
                  Logout
                </Button>
              )}

              {/* User Dropdown */}
              {isUserMenuOpen && (
                <div className="absolute right-0 top-full w-48 bg-popover border border-border rounded-lg shadow-dropdown py-1 animate-fade-in z-50">
                  {userMenuItems.map((item, index) => (
                    <div key={index}>
                      {item.path ? (
                        <Link
                          to={item.path}
                          onClick={() => setIsUserMenuOpen(false)}
                          className="flex items-center space-x-3 px-4 py-2 text-sm text-popover-foreground hover:bg-muted transition-colors duration-150"
                        >
                          <Icon name={item.icon} size={16} />
                          <span>{item.label}</span>
                        </Link>
                      ) : item.onClick ? (
                        <button
                          onClick={() => { item.onClick(); setIsUserMenuOpen(false); }}
                          className="w-full flex items-center space-x-3 px-4 py-2 text-sm text-popover-foreground hover:bg-muted transition-colors duration-150"
                        >
                          <Icon name={item.icon} size={16} />
                          <span>{item.label}</span>
                        </button>
                      ) : (
                        <button
                          onClick={() => handleUserAction(item.action)}
                          className="w-full flex items-center space-x-3 px-4 py-2 text-sm text-popover-foreground hover:bg-muted transition-colors duration-150"
                        >
                          <Icon name={item.icon} size={16} />
                          <span>{item.label}</span>
                        </button>
                      )}
                    </div>
                  ))}
                </div>
              )}
            </div>

            {/* Mobile Menu Button */}
            <button
              onClick={() => setIsMenuOpen(!isMenuOpen)}
              className="md:hidden p-2 rounded-lg text-muted-foreground hover:text-foreground hover:bg-muted transition-all duration-150"
            >
              <Icon name={isMenuOpen ? "X" : "Menu"} size={20} />
            </button>
          </div>
        </div>

        {/* Mobile Search */}
        {isSearchOpen && (
          <div className="md:hidden py-4 border-t border-border animate-fade-in">
            <form onSubmit={handleSearch} className="relative">
              <Input
                type="search"
                placeholder="Search products..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="w-full pl-10 pr-4"
                autoFocus
              />
              <Icon 
                name="Search" 
                size={18} 
                className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground pointer-events-none" 
              />
            </form>
          </div>
        )}

        {/* Mobile Menu */}
        {isMenuOpen && (
          <div className="md:hidden py-4 border-t border-border animate-fade-in">
            <nav className="space-y-2">
              {navigationItems.map((item) => (
                <Link
                  key={item.path}
                  to={item.path}
                  onClick={() => setIsMenuOpen(false)}
                  className={`flex items-center justify-between px-4 py-3 rounded-lg text-sm font-medium transition-all duration-150 ${
                    isActivePath(item.path)
                      ? 'text-primary bg-muted' :'text-muted-foreground hover:text-foreground hover:bg-muted'
                  }`}
                >
                  <div className="flex items-center space-x-3">
                    <Icon name={item.icon} size={18} />
                    <span>{item.label}</span>
                  </div>
                  {item.badge && (
                    <span className="bg-accent text-accent-foreground text-xs font-medium rounded-full h-5 w-5 flex items-center justify-center">
                      {item.badge}
                    </span>
                  )}
                </Link>
              ))}
            </nav>
          </div>
        )}
      </div>
    </header>
  );
};

export default Header;