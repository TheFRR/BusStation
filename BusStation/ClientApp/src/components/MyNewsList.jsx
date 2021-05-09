import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import { Report } from '@material-ui/icons';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    maxWidth: 750,
  },
  demo: {
    backgroundColor: theme.palette.background.paper,
  },
  title: {
    margin: theme.spacing(4, 0, 2),
  },
}));

export default function InteractiveList() {
  const classes = useStyles();

  return (
    <div className={classes.root}> 
      <Grid container>
        <Grid item style={{width: '300px', height: '500px', marginLeft: 'auto', marginRight: 'auto', marginBottom: '70px'}}>
          <Typography variant="h6" className={classes.title} align="left">
            Полезная информация и новости
          </Typography>
          <div className={classes.demo}>
            <List>
                <Button 
                variant="contained"
                color="primary"
                size="medium" href="https://covid19.rosminzdrav.ru/"
                style={{  width: '100%', margin: 'auto' }}>Информация о коронавирусе</Button>
                <Button 
                variant="contained"
                color="primary"
                size="medium" href="https://xn--b1aew.xn--p1ai/%D0%BD%D0%B5%D1%82-%D1%8D%D0%BA%D1%81%D1%82%D1%80%D0%B5%D0%BC%D0%B8%D0%B7%D0%BC%D1%83"
                style={{  width: '100%', margin: '10px auto' }}>Противодействие терроризму</Button>
                <ListItem>
                  <ListItemIcon>
                    <Report />
                  </ListItemIcon>
                  <ListItemText
                    primary="Внимание! Вторая станция закрыта на ремонт. Отправление автобусов временно происходит от первой станции"
                  />
                </ListItem>
                <ListItem>
                  <ListItemIcon>
                    <Report />
                  </ListItemIcon>
                  <ListItemText
                    primary="Рейсы до с. Жабное отменены"
                  />
                </ListItem>
                <ListItem>
                  <ListItemIcon>
                    <Report />
                  </ListItemIcon>
                  <ListItemText
                    primary="Рейсы до д. Лягушечная отменены"
                  />
                </ListItem>
            </List>
          </div>
        </Grid>
      </Grid>
    </div>
  );
}