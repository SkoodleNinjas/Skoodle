package Application.lobby;

import Application.room.CreateRoom;
import Application.room.Room;
import Application.room.RoomService;
import Application.room.name.RoomName;
import Application.room.name.RoomNameService;
import Application.room.topic.Topic;
import Application.room.topic.TopicService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.MessageMapping;
import org.springframework.messaging.handler.annotation.SendTo;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Created by ivan on 12.4.2017 г..
 */

@RestController
public class LobbyController {
    @Autowired
    private RoomService roomService;
    @Autowired
    private TopicService topicService;
    @Autowired
    RoomNameService roomNameService;

    @RequestMapping(value = "/rest/rooms", method = RequestMethod.POST)
    @MessageMapping("/add-room")
    @SendTo("/rest/add-room")
    public Room createAndGiveRoom(CreateRoom roomParams) throws  Exception{
        byte[] image = new byte[0];
        Topic t = new Topic();
        t.setTopic("Cat");
        t.setCategory("Animal");
        t.setImage(image);
        Topic t1 = new Topic();
        t.setTopic("Dog");
        t.setCategory("Animal");
        t.setImage(image);
        topicService.saveTopic(t);
        topicService.saveTopic(t1);
        RoomName rn = new RoomName();
        rn.setRoomName("Qkata staq");
        RoomName rn1 = new RoomName();
        rn.setRoomName("Ne6to");
        RoomName rn2 = new RoomName();
        rn.setRoomName("TUES");
        roomNameService.saveRoomName(rn);
        roomNameService.saveRoomName(rn1);
        roomNameService.saveRoomName(rn2);
        Room r = new Room();
        r.setMaxPlayers(roomParams.getMaxPlayers());
        r.setName(roomNameService.findRandom());
        r.setTopic(topicService.findRandom());
        roomService.saveRoom(r);
        return r;
    }


    @RequestMapping("/lobby")
    public List<Room> getRooms() throws  Exception{
        return roomService.findAll();
    }

}
