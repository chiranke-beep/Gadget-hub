import React from 'react';
import Button from '../../../components/ui/Button';

const LoadMoreButton = ({ onLoadMore, loading, hasMore }) => {
  if (!hasMore) {
    return (
      <div className="text-center py-8">
        <p className="text-muted-foreground">You've reached the end of our catalog</p>
      </div>
    );
  }

  return (
    <div className="text-center py-8">
      <Button
        variant="outline"
        size="lg"
        loading={loading}
        onClick={onLoadMore}
        iconName="ChevronDown"
        iconPosition="right"
      >
        {loading ? 'Loading more products...' : 'Load More Products'}
      </Button>
    </div>
  );
};

export default LoadMoreButton;