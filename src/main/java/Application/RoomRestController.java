package Application;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.context.request.WebRequest;

import java.util.List;

/**
 * Created by ivan on 28.4.2017 г..
 */

@RestController
public class RoomRestController {

    @Autowired
    private RoomService roomService;

    @RequestMapping(value = "/rest/rooms", method = RequestMethod.GET)
    public List<Room> getRooms() throws Exception {
        return roomService.findAll();
    }

    @RequestMapping(value = "/rest/room/{id}", method = RequestMethod.GET)
    public Room giveRoomFromId(@PathVariable long id) throws Exception {
        return roomService.findOne(id);
    }

    // TODO Ask how should this work
    @RequestMapping(value = "/rest/room/{id}", method = RequestMethod.PATCH)
    public Room editRoom(){
        return new Room();
    }

    //TODO Check this on the following url: https://spring.io/blog/2014/10/22/introducing-spring-sync
    @RequestMapping(value = "/rest/room/{id}", method = RequestMethod.POST)
    public void deleteRoom() {}
}
