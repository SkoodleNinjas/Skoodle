package Application;

import org.jboss.logging.Message;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.MessageMapping;
import org.springframework.messaging.handler.annotation.SendTo;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Created by ivan on 12.4.2017 г..
 */

@RestController
public class LobbyController {
    @Autowired
    private RoomService roomService;

    @RequestMapping(value = "/rest/rooms", method = RequestMethod.POST)
    @MessageMapping("/add-room")
    @SendTo("/rest/add-room")
    public Room createAndGiveRoom(CreateRoom roomParams) throws  Exception{
        Room r = new Room();
        r.setMaxPlayers(roomParams.getMaxPlayers());
        roomService.saveRoom(r);
        return r;
    }


    @RequestMapping("/lobby")
    public List<Room> getRooms() throws  Exception{
        return roomService.findAll();
    }

}
