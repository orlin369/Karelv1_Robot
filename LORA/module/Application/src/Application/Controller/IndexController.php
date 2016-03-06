<?php
/**
 * Zend Framework (http://framework.zend.com/)
 *
 * @link      http://github.com/zendframework/ZendSkeletonApplication for the canonical source repository
 * @copyright Copyright (c) 2005-2015 Zend Technologies USA Inc. (http://www.zend.com)
 * @license   http://framework.zend.com/license/new-bsd New BSD License
 */

namespace Application\Controller;

use Zend\Mvc\Controller\AbstractActionController;
use Zend\Mvc\View\Console\ViewManager;
use Zend\View\Model\ViewModel;

class IndexController extends AbstractActionController
{
    const DEFAULT_PATH = '/firebase/lora';

    public function indexAction()
    {

        $request = $this->getRequest();

        $firebase = new \Firebase\FirebaseLib('https://lora.firebaseio.com');

        if ($request->isPost())
        {
            $postData = $request->getPost()->toArray();

            if (!empty($postData))
            {
                if (isset($postData['robotData']))
                {
                    $robotData = $postData['robotData'];

                    $jsonData = json_decode($robotData);
                    $dateTime = new \DateTime();

                    try {
                        $firebase->set(self::DEFAULT_PATH . '/' . $dateTime->getTimestamp(), $jsonData);
                    } catch(Exception $e)
                    {
                        var_dump($e);
                    }
                }
            }

            print 'ninja';die;
        }

        return new ViewModel();
    }

    public function previewAction()
    {
        return new ViewModel();
    }
}
