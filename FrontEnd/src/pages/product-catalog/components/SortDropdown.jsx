import React, { useState, useRef, useEffect } from 'react';
import Icon from '../../../components/AppIcon';

const SortDropdown = ({ sortBy, onSortChange, totalResults }) => {
  const [isOpen, setIsOpen] = useState(false);
  const dropdownRef = useRef(null);

  const sortOptions = [
    { value: 'relevance', label: 'Most Relevant', icon: 'Star' },
    { value: 'price-low', label: 'Price: Low to High', icon: 'TrendingUp' },
    { value: 'price-high', label: 'Price: High to Low', icon: 'TrendingDown' },
    { value: 'popularity', label: 'Most Popular', icon: 'Heart' },
    { value: 'newest', label: 'Newest First', icon: 'Clock' }
  ];

  const currentSort = sortOptions.find(option => option.value === sortBy);

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
        setIsOpen(false);
      }
    };

    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  const handleSortSelect = (value) => {
    onSortChange(value);
    setIsOpen(false);
  };

  return (
    <div className="flex items-center justify-between mb-6">
      <div className="text-sm text-muted-foreground">
        Showing <span className="font-medium text-foreground">{totalResults}</span> products
      </div>
      
      <div className="relative" ref={dropdownRef}>
        <button
          onClick={() => setIsOpen(!isOpen)}
          className="flex items-center space-x-2 px-4 py-2 bg-card border border-border rounded-lg text-sm font-medium text-foreground hover:bg-muted transition-colors duration-200"
        >
          <Icon name={currentSort?.icon || 'ArrowUpDown'} size={16} />
          <span>Sort by: {currentSort?.label || 'Relevance'}</span>
          <Icon 
            name="ChevronDown" 
            size={16} 
            className={`transform transition-transform duration-200 ${isOpen ? 'rotate-180' : ''}`} 
          />
        </button>

        {isOpen && (
          <div className="absolute right-0 mt-2 w-56 bg-popover border border-border rounded-lg shadow-lg py-1 z-50 animate-fade-in">
            {sortOptions.map((option) => (
              <button
                key={option.value}
                onClick={() => handleSortSelect(option.value)}
                className={`w-full flex items-center space-x-3 px-4 py-2 text-sm transition-colors duration-150 ${
                  sortBy === option.value
                    ? 'bg-muted text-primary font-medium' :'text-popover-foreground hover:bg-muted'
                }`}
              >
                <Icon name={option.icon} size={16} />
                <span>{option.label}</span>
                {sortBy === option.value && (
                  <Icon name="Check" size={16} className="ml-auto" />
                )}
              </button>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default SortDropdown;