import React from 'react';
import Button from '@material-ui/core/Button';

export default function DisableElevation() {
  return (
    <div style={{  width: '80%', margin: ' 10px auto' }}>
        <Button>Редактировать</Button>
        <Button>Удалить</Button>
    </div>
  );
}