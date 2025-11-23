import React from 'react';
import { Link } from 'react-router-dom';
import Icon from '../../../components/AppIcon';

const Footer = () => {
  const currentYear = new Date().getFullYear();

  const footerSections = [
    {
      title: "Products",
      links: [
        { name: "Graphics Cards", href: "/product-catalog" },
        { name: "Processors", href: "/product-catalog" },
        { name: "Gaming Peripherals", href: "/product-catalog" },
        { name: "Storage Solutions", href: "/product-catalog" },
        { name: "Monitors", href: "/product-catalog" }
      ]
    },
    {
      title: "Services",
      links: [
        { name: "Quotation System", href: "/product-catalog" },
        { name: "Order Tracking", href: "/shopping-cart" },
        { name: "Technical Support", href: "#" },
        { name: "Warranty Service", href: "#" },
        { name: "Installation Guide", href: "#" }
      ]
    },
    {
      title: "Company",
      links: [
        { name: "About Us", href: "#" },
        { name: "Privacy Policy", href: "#" },
        { name: "Terms of Service", href: "#" },
        { name: "Blog", href: "#" },
        { name: "Careers", href: "#" }
      ]
    },
    {
      title: "Support",
      links: [
        { name: "Help Center", href: "#" },
        { name: "Contact Us", href: "#" },
        { name: "Live Chat", href: "#" },
        { name: "Return Policy", href: "#" },
        { name: "Shipping Info", href: "#" }
      ]
    }
  ];

  const socialLinks = [
    { name: "Facebook", icon: "Facebook", href: "#" },
    { name: "Twitter", icon: "Twitter", href: "#" },
    { name: "Instagram", icon: "Instagram", href: "#" },
    { name: "YouTube", icon: "Youtube", href: "#" },
    { name: "LinkedIn", icon: "Linkedin", href: "#" }
  ];

  const paymentMethods = [
    { name: "Visa", icon: "CreditCard" },
    { name: "Mastercard", icon: "CreditCard" },
    { name: "PayPal", icon: "Wallet" },
    { name: "Apple Pay", icon: "Smartphone" }
  ];

  return (
    <footer className="bg-gray-900 border-t border-gray-800">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        {/* Main Footer Content */}
        <div className="py-16">
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-6 gap-8">
            {/* Brand Section */}
            <div className="lg:col-span-2">
              <Link to="/home-page" className="flex items-center space-x-2 mb-6">
                <div className="w-10 h-10 bg-gradient-to-r from-blue-500 to-emerald-500 rounded-lg flex items-center justify-center">
                  <Icon name="Zap" size={24} color="white" />
                </div>
                <span className="text-2xl font-bold text-white">GadgetHub</span>
              </Link>
              
              <p className="text-gray-400 leading-relaxed mb-6 max-w-md">
                Your ultimate destination for premium PC components and cutting-edge gadgets. 
                Get instant quotations and build your dream tech setup today.
              </p>

              {/* Contact Info */}
              <div className="space-y-3">
                <div className="flex items-center space-x-3 text-gray-400">
                  <Icon name="Mail" size={16} />
                  <span>support@gadgethub.com</span>
                </div>
                <div className="flex items-center space-x-3 text-gray-400">
                  <Icon name="Phone" size={16} />
                  <span>+1 (555) 123-4567</span>
                </div>
                <div className="flex items-center space-x-3 text-gray-400">
                  <Icon name="MapPin" size={16} />
                  <span>123 Tech Street, Silicon Valley, CA 94025</span>
                </div>
              </div>
            </div>

            {/* Footer Links */}
            {footerSections.map((section, index) => (
              <div key={index}>
                <h3 className="text-white font-semibold mb-4">{section.title}</h3>
                <ul className="space-y-3">
                  {section.links.map((link, linkIndex) => (
                    <li key={linkIndex}>
                      <Link
                        to={link.href}
                        className="text-gray-400 hover:text-white transition-colors duration-200 text-sm"
                      >
                        {link.name}
                      </Link>
                    </li>
                  ))}
                </ul>
              </div>
            ))}
          </div>
        </div>

        {/* Newsletter Section */}
        <div className="py-8 border-t border-gray-800">
          <div className="grid md:grid-cols-2 gap-8 items-center">
            <div>
              <h3 className="text-xl font-semibold text-white mb-2">Stay Updated</h3>
              <p className="text-gray-400">Get the latest deals and tech news delivered to your inbox.</p>
            </div>
            <div className="flex flex-col sm:flex-row gap-3">
              <div className="flex-1">
                <input
                  type="email"
                  placeholder="Enter your email"
                  className="w-full px-4 py-3 bg-gray-800 border border-gray-700 rounded-lg text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                />
              </div>
              <button className="px-6 py-3 bg-gradient-to-r from-blue-500 to-emerald-500 text-white font-medium rounded-lg hover:from-blue-600 hover:to-emerald-600 transition-all duration-200 flex items-center justify-center space-x-2">
                <span>Subscribe</span>
                <Icon name="ArrowRight" size={16} />
              </button>
            </div>
          </div>
        </div>

        {/* Bottom Footer */}
        <div className="py-8 border-t border-gray-800">
          <div className="flex flex-col lg:flex-row justify-between items-center space-y-4 lg:space-y-0">
            {/* Copyright */}
            <div className="text-gray-400 text-sm">
              © {currentYear} GadgetHub. All rights reserved.
            </div>

            {/* Social Links */}
            <div className="flex items-center space-x-4">
              <span className="text-gray-400 text-sm mr-2">Follow us:</span>
              {socialLinks.map((social, index) => (
                <a
                  key={index}
                  href={social.href}
                  className="w-10 h-10 bg-gray-800 hover:bg-gray-700 rounded-lg flex items-center justify-center text-gray-400 hover:text-white transition-all duration-200"
                  aria-label={social.name}
                >
                  <Icon name={social.icon} size={18} />
                </a>
              ))}
            </div>

            {/* Payment Methods */}
            <div className="flex items-center space-x-4">
              <span className="text-gray-400 text-sm mr-2">We accept:</span>
              {paymentMethods.map((method, index) => (
                <div
                  key={index}
                  className="w-10 h-10 bg-gray-800 rounded-lg flex items-center justify-center text-gray-400"
                  title={method.name}
                >
                  <Icon name={method.icon} size={18} />
                </div>
              ))}
            </div>
          </div>
        </div>

        {/* Trust Badges */}
        <div className="py-6 border-t border-gray-800">
          <div className="flex flex-wrap justify-center items-center space-x-8 space-y-2">
            <div className="flex items-center space-x-2 text-gray-400">
              <Icon name="Shield" size={16} className="text-green-400" />
              <span className="text-sm">SSL Secured</span>
            </div>
            <div className="flex items-center space-x-2 text-gray-400">
              <Icon name="Award" size={16} className="text-blue-400" />
              <span className="text-sm">Authorized Dealer</span>
            </div>
            <div className="flex items-center space-x-2 text-gray-400">
              <Icon name="Truck" size={16} className="text-emerald-400" />
              <span className="text-sm">Fast Shipping</span>
            </div>
            <div className="flex items-center space-x-2 text-gray-400">
              <Icon name="RefreshCw" size={16} className="text-yellow-400" />
              <span className="text-sm">30-Day Returns</span>
            </div>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;