package Application;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.transaction.Transactional;
import java.util.List;
import java.util.Random;

/**
 * Created by nikolay on 4/27/17.
 */
@Service
@Transactional
public class TopicServiceImpl implements TopicService {
    @Autowired
    private TopicRepository topicRepository;

    @Override
    public List<Topic> findAll() {
        return topicRepository.findAll();
    }

    @Override
    public Topic findOne(long id) {
        return topicRepository.findOne(id);
    }

    @Override
    public Topic findRandom() {
        Random random = new Random();
        return topicRepository.findOne((long) (random.nextInt((int) ((topicRepository.count() - 1) - 0 + 1)) + 0));
    }

    @Override
    public void saveTopic(Topic topic) {
        topicRepository.save(topic);
    }
}
